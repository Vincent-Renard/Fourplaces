using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;
using Model.Dtos;
namespace FourplacesApp
{
    public interface IRestService
    {
        /*
         * 12/13
         * Le refresh est interne (on l'appelle jamais explicitement)
         *        
         */
              

        /*
         * WELCOME
         */
        void GetRoot();


        /*
         * USER
         */
        Task<bool> Login(LoginRequest log_user);
        Task<LoginResult> Signin(RegisterRequest user);
        Task<UserItem> GetMe();
        Task<UserItem> PatchMe(UpdateProfileRequest patch_user);
        Task<UserItem> PatchPassword(string updatePassword);
        /*
         *PLACE
         */
        string GetImage(int idImg);
        Task<List<PlaceItemSummary>> GetListPlacesAsync();
        // Task<Response> PostImg(CreatePlaceRequest placeRequest); //TOKEN
        Task<Response> PostPlaceAsync(CreatePlaceRequest placeRequest);
        Task<PlaceItem> GetPlace(int idPlace);
        Task<Response> PostCommentAsync(int idPlace,CreateCommentRequest commentRequest);
    }
}