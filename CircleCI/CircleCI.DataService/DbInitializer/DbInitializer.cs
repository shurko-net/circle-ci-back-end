using CircleCI.DataService.Data;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace CircleCI.DataService.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _context;

    public DbInitializer(AppDbContext context)
    {
        _context = context;
    }
    public void Initialize()
    {
        if (!_context.Database.CanConnect()
            || !_context.Database.GetPendingMigrations().Any())
        {
            _context.Database.Migrate();
        }
        if (!_context.CategoriesList.Any())
        {
            _context.CategoriesList.AddRange(new List<CategoryList>()
            {
                new()
                {
                    Name = "C#",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/c-sharp-c-icon.png"
                },
                new()
                {
                    Name = "C++",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/cplusplus.png"
                },
                new()
                {
                    Name = "C",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/c_original_logo_icon_146611.png"
                },
                new()
                {
                    Name = "F#",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/fsharp.png"
                },
                new()
                {
                    Name = "Email",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/emailicon.png"
                },
                new()
                {
                    Name = "Google Workspace",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/googleWorkspace.png"
                },
                new()
                {
                    Name = "Google Play",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/google-play-store-logo-png-transparent-png-logos-10.png"
                },
                new()
                {
                    Name = "Java",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/java.png"
                },
                new()
                {
                    Name = "JavaScript",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/js.png"
                },
                new()
                {
                    Name = "Unity",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/unity-icon.png"
                },
                new()
                {
                    Name = "Angular",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Angular.png"
                },
                new()
                {
                    Name = "React",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/react.png"
                },
                new()
                {
                    Name = "Python",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Python_logo_icon.png"
                },
                new()
                {
                    Name = "Microsoft",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/microsoft.png"
                },
                new()
                {
                    Name = "Json Web Token",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/jwt.png"
                },
                new()
                {
                    Name = "HTML",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/html.png"
                },
                new()
                {
                    Name = "CSS",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/css.png"
                },
                new()
                {
                    Name = "Linux",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/linux.png"
                },
                new()
                {
                    Name = "Google",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/google-icon.png"
                },
                new()
                {
                    Name = "Node.js",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/node-js.png"
                },
                new()
                {
                    Name = "SQL",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/sql.png"
                },
                new()
                {
                    Name = "Git",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Git_icon.png"
                },
                new()
                {
                    Name = "GitHub",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/github.png"
                },
                new()
                {
                    Name = "API",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/api.png"
                },
                new()
                {
                    Name = "Nginx",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/ngnix.png"
                },
                new()
                {
                    Name = "IT",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/it.png"
                },
                new()
                {
                    Name = "macOS",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/MacOS_logo.png"
                },
                new()
                {
                    Name = ".Net",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Microsoft_.NET_logo.png"
                },
                new()
                {
                    Name = "Ruby",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Ruby_logo.png"
                },
                new()
                {
                    Name = "Amazon Web Services",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/aws.png"
                },
                new()
                {
                    Name = "Vue.js",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/vuejs.png"
                },
                new()
                {
                    Name = "TypeScript",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Typescript_logo.png"
                },
                new()
                {
                    Name = "Microsoft Azure",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/azure.png"
                },
                new()
                {
                    Name = "Accessibility",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/access.png"
                },
                new()
                {
                    Name = "Docker",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Docker-Symbol.png"
                },
                new()
                {
                    Name = "Kubernetes",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Kubernetes-Logo.png"
                },
                new()
                {
                    Name = "Linus Torvalds",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/glek.png"
                },
            });
            
            _context.SaveChanges();
        }
    }
}