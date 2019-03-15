using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Dtos;

namespace FourplacesApp
{
    public interface IRestService
    {
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
        Task<UserItem> PatchPassword(UpdatePasswordRequest updatePassword);
        /*
         *PLACE
         */
        Task<string> GetImage(int idImg);
        Task<List<PlaceItemSummary>> GetListPlacesAsync();
        // Task<Response> PostImg(CreatePlaceRequest placeRequest); //TOKEN
        Task<Response> PostPlace(CreatePlaceRequest placeRequest);

        Task<PlaceItem> GetPlace(int idPlace);
        Task<Response> PostCommentAsync(int idPlace,CreateCommentRequest commentRequest);

    }
}