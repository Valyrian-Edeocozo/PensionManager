using System;
using Microsoft.AspNetCore.Mvc;

namespace PensionManager.PensionManager.Api;

    [Route("api/[controller]")]
    [ApiController]
    // [JwtAuthentication]
    public abstract class ApiControllerBase : ControllerBase
    {
        
    }
