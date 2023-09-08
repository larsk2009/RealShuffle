using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SpotifyAPI.Web;

namespace RealShuffle.Pages
{
  public partial class Index
  {
    [Inject]
    public NavigationManager navManager { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      var baseUri = new Uri(navManager.ToAbsoluteUri(navManager.BaseUri), "callback");

      var loginRequest = new LoginRequest(baseUri, "95e53ca982ba42d6820dfac81e60ee5a", LoginRequest.ResponseType.Token)
      { Scope = new[] { Scopes.PlaylistReadCollaborative, Scopes.PlaylistReadPrivate, Scopes.PlaylistModifyPublic, Scopes.PlaylistModifyPublic } };
      var uri = loginRequest.ToUri();
      NavManager.NavigateTo(uri.AbsoluteUri);
      await base.OnAfterRenderAsync(firstRender);
    }
  }
}