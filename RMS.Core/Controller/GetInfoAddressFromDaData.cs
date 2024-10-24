using Dadata;
using Dadata.Model;

namespace RMS.Core.Controller
{
    public static class GetInfoAddressFromDaData
    {        
        public static SuggestResponse<Address> UpdateFromDaData(string token, string value)
        {            
            var suggestClient = new SuggestClient(token);
            var response = suggestClient.SuggestAddress(value);

            if (response.suggestions.Count <= 0)
            {
                return null;
            }

            //var address = response.suggestions[0]?.data;
            return response;
        }        
    }
}
