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
        if (!_context.Database.GetPendingMigrations().Any())
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
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/c-sharp-c-icon.png"
                },
                new CategoryList
                {
                    Name = "C++",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/cplusplus.png"
                },
                new CategoryList
                {
                    Name = "C",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/c_original_logo_icon_146611.png"
                },
                new CategoryList
                {
                    Name = "F#",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/fsharp.png"
                },
                new CategoryList
                {
                    Name = "Email",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/emailicon.png"
                },
                new CategoryList
                {
                    Name = "Google Workspace",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/googleWorkspace.png"
                },
                new CategoryList
                {
                    Name = "Google Play",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/google-play-store-logo-png-transparent-png-logos-10.png"
                },
                new CategoryList
                {
                    Name = "Java",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/java.png"
                },
                new CategoryList
                {
                    Name = "JavaScript",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/js.png"
                },
                new CategoryList
                {
                    Name = "Unity",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/unity-icon.png"
                },
                new CategoryList
                {
                    Name = "Angular",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Angular.png"
                },
                new CategoryList
                {
                    Name = "React",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/react.png"
                },
                new CategoryList
                {
                    Name = "Python",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Python_logo_icon.png"
                },
                new CategoryList
                {
                    Name = "Microsoft",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/microsoft.png"
                },
                new CategoryList
                {
                    Name = "Json Web Token",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/jwt.png"
                },
                new CategoryList
                {
                    Name = "HTML",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/html.png"
                },
                new CategoryList
                {
                    Name = "CSS",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/css.png"
                },
                new CategoryList
                {
                    Name = "Linux",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/linux.png"
                },
                new CategoryList
                {
                    Name = "Google",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/google-icon.png"
                },
                new CategoryList
                {
                    Name = "Node.js",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/node-js.png"
                },
                new CategoryList
                {
                    Name = "SQL",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/sql.png"
                },
                new CategoryList
                {
                    Name = "Git",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Git_icon.png"
                },
                new CategoryList
                {
                    Name = "GitHub",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/github.png"
                },
                new CategoryList
                {
                    Name = "API",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/api.png"
                },
                new CategoryList
                {
                    Name = "Nginx",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/ngnix.png"
                },
                new CategoryList
                {
                    Name = "IT",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/it.png"
                },
                new CategoryList
                {
                    Name = "macOS",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/MacOS_logo.png"
                },
                new CategoryList
                {
                    Name = ".Net",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Microsoft_.NET_logo.png"
                },
                new CategoryList
                {
                    Name = "Ruby",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Ruby_logo.png"
                },
                new CategoryList
                {
                    Name = "Amazon Web Services",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/aws.png"
                },
                new CategoryList
                {
                    Name = "Vue.js",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/vuejs.png"
                },
                new CategoryList
                {
                    Name = "TypeScript",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Typescript_logo.png"
                },
                new CategoryList
                {
                    Name = "Microsoft Azure",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/azure.png"
                },
                new CategoryList
                {
                    Name = "Accessibility",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/access.png"
                },
                new CategoryList
                {
                    Name = "Docker",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Docker-Symbol.png"
                },
                new CategoryList
                {
                    Name = "Kubernetes",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/Kubernetes-Logo.png"
                },
                new CategoryList
                {
                    Name = "Linus Torvalds",
                    ImageUrl = "https://storage.googleapis.com/circle-ci-bucket/IconsForCategoryList/glek.png"
                },
            });
            _context.SaveChanges();
        }
    }
}