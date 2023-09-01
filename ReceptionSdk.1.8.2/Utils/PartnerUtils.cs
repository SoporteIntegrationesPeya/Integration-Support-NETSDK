using ReceptionSdk.Clients;
using ReceptionSdk.Exceptions;
using ReceptionSdk.Helpers;
using ReceptionSdk.Http;
using ReceptionSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceptionSdk.Utils
{
    public static class PartnerUtils
    {
        public static long matchRestaurantByIntegrationCode(ApiConnection connection, string restaurantCode)
        {
            List<Restaurant> restaurants = GetRestaurants(connection);
            Restaurant restaurant = restaurants.Find(res => res.IntegrationCode == restaurantCode);
            if (restaurant != null && restaurant.Id != 0)
            {
                return restaurant.Id;
            }
            throw new ArgumentNullException("You must specify a valid restaurantCode");
        }

        public static List<Restaurant> GetRestaurants(ApiConnection connection)
        {
            try
            {
                RestaurantsClient restaurantClient = new RestaurantsClient(connection);
                PaginationOptions options = PaginationOptions.Create();
                return new List<Restaurant>(restaurantClient.GetAll(options.Next()));
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
