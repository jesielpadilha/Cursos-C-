﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Data
{
    public class ApplicationDbContext : IdentityDbContext, ISecurityKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}