using Microsoft.EntityFrameworkCore;

namespace AppTemplate.Infrastructure.Data;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
}
