using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure;
using Chirp.Core;

namespace Chirp.Web.Pages;

/// <summary>
/// Razor page arepresenting an authors user information page.
/// </summary>
public class UserInformationModel : PageModel
{
    private readonly UserManager<Author> _userManager;
    private readonly SignInManager<Author> _signInManager;
    private readonly IAuthorRepository _authorRepo;

    /// <summary>
    /// Gets or sets the following of an author.
    /// </summary>
    public List<AuthorDTO>? Following { get; set; }

    /// <summary>
    /// Initializes a new instance of public timeline.
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="authorRepo"></param>
    public UserInformationModel(UserManager<Author> userManager, SignInManager<Author> signInManager, IAuthorRepository authorRepo)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authorRepo = authorRepo;
    }

    /// <summary>
    /// Handles HTTP GET requests for the user information page.
    /// </summary>
    /// <returns>The IActionResult representing the result of the operation.</returns>
    public async Task<ActionResult> OnGet()
    {
        Following = await _authorRepo.GetFollowing(User.Identity?.Name!);
        return Page();
    }

    /// <summary>
    /// Handles HTTP POST requests for the user information page.
    /// </summary>
    /// <returns>The IActionResult representing the result of the operation.</returns>
    public async Task<IActionResult> OnPostForgetUser()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();
        }
        return LocalRedirect(Url.Content("~/"));
    }
}

