using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PensionManager.PensionManager.Infrastructure;
using PensionManager.PensionManger.Domain;
using PensionManager.PensionManger.Domain.Dtos;

namespace PensionManager.PensionManager.Api;

public class UserManagementController(UserManager<User> userManager, ApplicationDbContext context) : ApiControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    [Authorize] // Ensure only authenticated users can access this endpoint
    [HttpPut("update")]
    public async Task<IActionResult> UpdateMemberDetails([FromBody] UpdateUserRequest model)
    {
        // Get the current user's ID from the JWT token
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new UpdateUserResponse { Success = false, Message = "User not authenticated." });
        }

        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound(new UpdateUserResponse { Success = false, Message = "User not found." });
        }

        // Update user properties
        user.FirstName = model.FirstName ?? user.FirstName;
        user.LastName = model.LastName ?? user.LastName;
        user.DateOfBirth = model.DateOfBirth ?? user.DateOfBirth;
        user.Address = model.Address ?? user.Address;
        user.EmployerId = model.EmployerId ?? user.EmployerId;
        user.PensionPlanId = model.PensionPlanId ?? user.PensionPlanId;
        user.DateModified = DateTime.Now;

        // Save changes
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(new UpdateUserResponse { Success = false, Message = "Failed to update user." });
        }

        return Ok(new UpdateUserResponse { Success = true, Message = "User updated successfully." });

    }

    public async Task<IActionResult> DeleteUser()
    {
         // Get the current user's ID from the JWT token
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { Success = false, Message = "User not authenticated." });
        }

        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound(new { Success = false, Message = "User not found." });
        }

        // Soft delete the user by setting IsDeleted to true
        user.IsDeleted = true;
        user.DateModified = DateTime.Now;

        // Save changes
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(new { Success = false, Message = "Failed to delete user." });
        }

        return Ok(new { Success = true, Message = "User deleted successfully." });
    }

    public async Task<IActionResult> GetUser()
    {
        // Get all users who are not soft-deleted and include their pension plan details
        var users = await _context.Users
            .Where(u => !u.IsDeleted)
            .Join(
                _context.PensionPlans,
                user => user.PensionPlanId,
                pensionPlan => pensionPlan.PensionPlanId,
                (user, pensionPlan) => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    NationalId = user.NationalId,
                    Address = user.Address,
                    EmployerId = user.EmployerId,
                    PensionPlanId = user.PensionPlanId,
                    PensionPlanDetails = new PensionPlanDto
                    {
                        PensionPlanId = pensionPlan.PensionPlanId,
                        PlanName = pensionPlan.PlanName,
                        Details = pensionPlan.Details
                    },
                    DateCreated = user.DateCreated,
                    DateModified = user.DateModified
                })
            .ToListAsync();

        return Ok(new GetUsersResponse
        {
            Success = true,
            Message = "Users retrieved successfully.",
            Users = users
        });
    }

}
