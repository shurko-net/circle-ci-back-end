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
                new CategoryList
                {
                    Name = "C#",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/c-sharp-c-icon.png"
                },
                new CategoryList
                {
                    Name = "C++",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/cplusplus.png"
                },
                new CategoryList
                {
                    Name = "C",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/c_original_logo_icon_146611.png"
                },
                new CategoryList
                {
                    Name = "F#",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/fsharp.png"
                },
                new CategoryList
                {
                    Name = "Email",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/emailicon.png"
                },
                new CategoryList
                {
                    Name = "Google Workspace",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/googleWorkspace.png"
                },
                new CategoryList
                {
                    Name = "Google Play",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/google-play-store-logo-png-transparent-png-logos-10.png"
                },
                new CategoryList
                {
                    Name = "Java",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/java.png"
                },
                new CategoryList
                {
                    Name = "JavaScript",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/js.png"
                },
                new CategoryList
                {
                    Name = "Unity",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/unity-icon.png"
                },
                new CategoryList
                {
                    Name = "Angular",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Angular.png"
                },
                new CategoryList
                {
                    Name = "React",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/react.png"
                },
                new CategoryList
                {
                    Name = "Python",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Python_logo_icon.png"
                },
                new CategoryList
                {
                    Name = "Microsoft",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/microsoft.png"
                },
                new CategoryList
                {
                    Name = "Json Web Token",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/jwt.png"
                },
                new CategoryList
                {
                    Name = "HTML",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/html.png"
                },
                new CategoryList
                {
                    Name = "CSS",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/css.png"
                },
                new CategoryList
                {
                    Name = "Linux",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/linux.png"
                },
                new CategoryList
                {
                    Name = "Google",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/google-icon.png"
                },
                new CategoryList
                {
                    Name = "Node.js",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/node-js.png"
                },
                new CategoryList
                {
                    Name = "SQL",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/sql.png"
                },
                new CategoryList
                {
                    Name = "Git",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Git_icon.png"
                },
                new CategoryList
                {
                    Name = "GitHub",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/github.png"
                },
                new CategoryList
                {
                    Name = "API",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/api.png"
                },
                new CategoryList
                {
                    Name = "Nginx",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/ngnix.png"
                },
                new CategoryList
                {
                    Name = "IT",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/it.png"
                },
                new CategoryList
                {
                    Name = "macOS",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/MacOS_logo.png"
                },
                new CategoryList
                {
                    Name = ".Net",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Microsoft_.NET_logo.png"
                },
                new CategoryList
                {
                    Name = "Ruby",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Ruby_logo.png"
                },
                new CategoryList
                {
                    Name = "Amazon Web Services",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/aws.png"
                },
                new CategoryList
                {
                    Name = "Vue.js",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/vuejs.png"
                },
                new CategoryList
                {
                    Name = "TypeScript",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Typescript_logo.png"
                },
                new CategoryList
                {
                    Name = "Microsoft Azure",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/azure.png"
                },
                new CategoryList
                {
                    Name = "Accessibility",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/access.png"
                },
                new CategoryList
                {
                    Name = "Docker",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Docker-Symbol.png"
                },
                new CategoryList
                {
                    Name = "Kubernetes",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/Kubernetes-Logo.png"
                },
                new CategoryList
                {
                    Name = "Linus Torvalds",
                    ImageUrl = "https://storage.googleapis.com/circleci-images/IconsForCategory/glek.png"
                },
            });
            
            _context.SaveChanges();
        }
    }
}