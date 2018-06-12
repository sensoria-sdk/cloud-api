using Sensoria.SmokeTest.Api.Helpers;
using Sensoria.SmokeTest.Api.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Sensoria.SmokeTest.Api.Tests
{
    public class TestShoeCloset
    {
        CommandLineOptions clo;
        string accessToken;
        User user;

        public TestShoeCloset(CommandLineOptions clo, string accessToken, User user)
        {
            this.clo = clo;
            this.accessToken = accessToken;
            this.user = user;
        }

        public void Run()
        {
            //Get the list of shoes in the user Shoe Closet
            Console.WriteLine("****** SHOE CLOSET TEST ******");

            SensoriaApiResult<Closet> resultGetShoeCloset = GetShoeCloset();
            Trace.Assert(resultGetShoeCloset.IsSuccess == true);
            Trace.Assert(resultGetShoeCloset.APIResult != null);
            Console.WriteLine("Get ShoeCloset correctly done");

            #region Adds a ShoeClosetItem (a shoe) to the user Shoe Closet
            ClosetItem sItem = new ClosetItem();
            sItem.ProductName = "adissage";
            sItem.ProductId = 77;
            sItem.ImageUrl = "https://secure-www.zappos.com/images/717/7172500/7768-31862-5.jpg";
            sItem.BrandName = "adidas";
            sItem.UserId = user.UserId;
            SensoriaApiResult<ClosetItem> apireturn = AddNewShoe(sItem);
            int newShoeClosetItemId = apireturn.APIResult.Id;
            Trace.Assert(apireturn.IsSuccess);
            Trace.Assert(apireturn.APIResult.Owned == true);
            Trace.Assert(apireturn.StatusCode == System.Net.HttpStatusCode.Created);
            Trace.Assert(newShoeClosetItemId > 0);
            Console.WriteLine("ShoeClosetItem correctly added to the user's shoe closet");
            #endregion

            #region Retrieve the shoe closet
            SensoriaApiResult<Closet> resultGetNewShoeClosetItem = GetShoeCloset(newShoeClosetItemId);
            Trace.Assert(resultGetNewShoeClosetItem.IsSuccess == true);
            Trace.Assert(resultGetNewShoeClosetItem.APIResult.ClosetItems.Count == 1);
            Trace.Assert(resultGetNewShoeClosetItem.APIResult.ClosetItems[0].ProductId == sItem.ProductId);
            Trace.Assert(resultGetNewShoeClosetItem.APIResult.ClosetItems[0].UserId == user.UserId);
            Console.WriteLine("Retrieved the inserted shoe from shoecloset");
            #endregion

            #region Edit two properties (Price amd RateSizeFit) of the previously added ShoeClosetItem
            sItem = resultGetNewShoeClosetItem.APIResult.ClosetItems[0];
            sItem.Recommend = true;
            sItem.RateComfort = 4;
            SensoriaApiResult<ClosetItem> editcall = EditShoeDetails(sItem);
            Trace.Assert(editcall.IsSuccess);
            #endregion

            #region Retrieve again the updated ShoeClosetItem and verify the updated values
            SensoriaApiResult<Closet> resultGetShoeClosetItem = GetShoeCloset(newShoeClosetItemId);
            Trace.Assert(resultGetShoeClosetItem.IsSuccess == true);
            Trace.Assert(resultGetShoeClosetItem.APIResult.ClosetItems.Count == 1);
            Trace.Assert(resultGetShoeClosetItem.APIResult.ClosetItems[0].Recommend == sItem.Recommend);
            Trace.Assert(resultGetShoeClosetItem.APIResult.ClosetItems[0].RateComfort == sItem.RateComfort);
            Console.WriteLine("Recommend amd RateComfort properties of the shoe has been correctly modified");
            #endregion

            #region Replace the updated ShoeClosetItem
            ClosetItem sItem2 = resultGetShoeClosetItem.APIResult.ClosetItems[0];
            SensoriaApiResult<ClosetItem> resultReplacedItem = ReplaceShoe(sItem2);
            int replacedItemId = resultReplacedItem.APIResult.Id;
            Trace.Assert(resultReplacedItem.IsSuccess == true);
            Console.WriteLine("The new ShoeClosetItem has been correctly replaced in the user's shoe closet");
            #endregion

            #region Retire the previously replaced ShoeClosetItem
            SensoriaApiResult<ClosetItem> resultRetireItem = RetireShoe(resultReplacedItem.APIResult);
            Trace.Assert(resultRetireItem.IsSuccess == true);
            Console.WriteLine("The replaced ShoeClosetItem has been correctly retired from the user's shoe closet");
            #endregion

            #region Edit RetiredDate (set back to null) property of the previously retired ShoeClosetItem
            int retiredItemId = resultRetireItem.APIResult.Id;
            SensoriaApiResult<Closet> resultRetiredItem = GetShoeCloset(retiredItemId);
            ClosetItem sretiredItem = resultRetiredItem.APIResult.ClosetItems[0];
            sretiredItem.RetiredDate = null;
            SensoriaApiResult<ClosetItem> editRetiredItem = EditShoeDetails(sretiredItem);
            Trace.Assert(editRetiredItem.IsSuccess);
            Console.WriteLine("The retired ShoeClosetItem has been correctly un-retired to the user's shoe closet");
            #endregion

            #region Delete the unretired ShoeClosetItem
            SensoriaApiResult<bool> deleteCall = DeleteShoe(retiredItemId);
            Trace.Assert(deleteCall.IsSuccess);
            Console.WriteLine("The unretired ShoeClosetItem has been correctly deleted from the user's shoe closet");
            #endregion

            #region Delete the ShoeClosetItem initially created in the shoeCloset
            SensoriaApiResult<bool> deleteCall2 = DeleteShoe(newShoeClosetItemId);
            Trace.Assert(deleteCall2.IsSuccess);
            Console.WriteLine("The ShoeClosetItem initially created has been correctly deleted from the user's shoe closet");
            #endregion

            #region Adds a ShoeClosetItem to the user WhishList
            ClosetItem sItemWL = new ClosetItem();
            sItemWL.ProductName = "adissage";
            sItemWL.ProductId = 77;
            sItemWL.ImageUrl = "https://secure-www.zappos.com/images/717/7172500/7768-31862-5.jpg";
            sItemWL.BrandName = "adidas";
            sItemWL.UserId = user.UserId;
            sItemWL.Owned = false;
            SensoriaApiResult<ClosetItem> apiWLreturn = AddNewShoe(sItemWL);
            int newShoeWishListItemId = apiWLreturn.APIResult.Id;
            Trace.Assert(apiWLreturn.IsSuccess);
            Trace.Assert(apiWLreturn.StatusCode == System.Net.HttpStatusCode.Created);
            Trace.Assert(newShoeWishListItemId > 0);
            Trace.Assert(apiWLreturn.APIResult.Owned  == false);
            Console.WriteLine("ShoeClosetItem correctly added to the user's wishlist");
            #endregion

            #region Retrieve the Shoe Wishlist
            SensoriaApiResult<Closet> resultGetWishList = GetShoeCloset(newShoeWishListItemId);
            Trace.Assert(resultGetWishList.IsSuccess == true);
            Trace.Assert(resultGetWishList.APIResult.ClosetItems.Count == 1);
            Trace.Assert(resultGetWishList.APIResult.ClosetItems[0].ProductId == sItemWL.ProductId);
            Trace.Assert(resultGetWishList.APIResult.ClosetItems[0].UserId == user.UserId);
            Console.WriteLine("Retrieved the inserted shoe from WishList");
            #endregion

            #region Delete the ShoeClosetItem initially created in the Shoe WishList
            SensoriaApiResult<bool> deleteShoeFromWL = DeleteShoe(newShoeWishListItemId);
            Trace.Assert(deleteShoeFromWL.IsSuccess);
            Console.WriteLine("The ShoeClosetItem initially created has been correctly deleted from the user's wishlist");
            #endregion

            #region Finally GetShoeCloset
            SensoriaApiResult<Closet> lastGetShoeCloset = GetShoeCloset();
            Trace.Assert(lastGetShoeCloset.IsSuccess == true);
            Trace.Assert(lastGetShoeCloset.APIResult != null);
            Console.WriteLine("Last Get ShoeCloset correctly done");
            #endregion

        }

        #region ShoeCloset Methods
        private SensoriaApiResult<Closet> GetShoeCloset(int shoeClosetItemId = -1)
        {
            ApiResultHttpClientHelper<Closet> client = new ApiResultHttpClientHelper<Closet>();

            if (shoeClosetItemId == -1)
                client.Url = String.Format("closet/shoes");
            else
                client.Url = String.Format("closet/shoes/{0}", shoeClosetItemId);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;
            
            return client.GetSensoriaApiResult();

        }
        private SensoriaApiResult<ClosetItem> AddNewShoe(ClosetItem newShoeClosetItem)
        {
            ApiResultHttpClientHelper<ClosetItem> client = new ApiResultHttpClientHelper<ClosetItem>();

            client.Url = String.Format("closet/shoes");
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;
            return client.PostSensoriaApiResult(newShoeClosetItem);
        }
        private SensoriaApiResult<ClosetItem> ReplaceShoe(ClosetItem newShoeClosetItem)
        {
            ApiResultHttpClientHelper<ClosetItem> client = new ApiResultHttpClientHelper<ClosetItem>();

            client.Url = String.Format("closet/shoes/{0}?action=replace", newShoeClosetItem.Id);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            return client.PutSensoriaApiResult(null);
        }
        private SensoriaApiResult<ClosetItem> EditShoeDetails(ClosetItem shoeClosetItem)
        {
            ApiResultHttpClientHelper<ClosetItem> client = new ApiResultHttpClientHelper<ClosetItem>();

            client.Url = String.Format("closet/shoes/{0}", shoeClosetItem.Id);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;
            return client.PutSensoriaApiResult(shoeClosetItem);
        }
        private SensoriaApiResult<ClosetItem> RetireShoe(ClosetItem shoeClosetItem)
        {
            ApiResultHttpClientHelper<ClosetItem> client = new ApiResultHttpClientHelper<ClosetItem>();

            client.Url = String.Format("closet/shoes/{0}?action=retire", shoeClosetItem.Id);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            return client.PutSensoriaApiResult(null);
        }
        private SensoriaApiResult<bool> DeleteShoe(int shoeClosetItemId)
        {
            ApiResultHttpClientHelper<bool> client = new ApiResultHttpClientHelper<bool>();

            client.Url = String.Format("closet/shoes/{0}", shoeClosetItemId);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            return client.DeleteSensoriaApiResult();
        }

        #endregion
    }
}
