using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using RealShuffle;
using RealShuffle.Shared;
using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;

namespace RealShuffle.Pages
{
    public partial class Index
    {
        [Inject]
        public IConfiguration Configuration { get; set; }
        
        [Inject]
        public NavigationManager NavManager { get; set; }

        public List<SimplePlaylist> Playlists { get; set; }

        private bool _isAuthed = false;
        private string _authCode;
        private Uri _authUri;

        protected override void OnInitialized()
        {
            if (!string.IsNullOrEmpty(Configuration["SPOTIFY_CLIENT_ID"]))
            {
                var baseUri = NavManager.ToAbsoluteUri(NavManager.BaseUri);

                var loginRequest = new LoginRequest(baseUri, Configuration["SPOTIFY_CLIENT_ID"], LoginRequest.ResponseType.Token)
                {
                    Scope = new[] { Scopes.PlaylistModifyPrivate, Scopes.PlaylistModifyPublic, Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative }
                };

                _authUri = loginRequest.ToUri();
            }
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            var queryString = new Uri(NavManager.Uri).Fragment;
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);
            _authCode = queryDictionary["#access_token"];

            if (_authCode is { })
            {
                _isAuthed = true;
                //TOOD store authCode in local storage\

                var spotify = new SpotifyClient(_authCode);
                Playlists = (await spotify.Playlists.CurrentUsers()).Items;

                StateHasChanged();
            }

            await base.OnInitializedAsync();
        }

        private void PlaylistClicked(SimplePlaylist simplePlaylist)
        {

        }
    }
}