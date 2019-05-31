#define OFFLINESYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecord.Helpers
{
    // TODO should really be called AzureAppServiceHelper - re Azure App Service
    public class AzureAppHelper
    {
        public static async Task SyncAsync()
        {
            IReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;        // to hold any errors arising
            try
            {
                await App.MobileService.SyncContext.PushAsync();            // push any changes from local to cloud (organised by package Microsoft.WindowsAzure.MobileServices)

                await App.postsTable.PullAsync("userPosts", "");            // pull (to, and using member table) from cloud, to synchronise
                    // queryId:userPosts
            }
            catch(MobileServicePushFailedException mspfe)
            {
                if(mspfe.PushResult != null)
                {
                    syncErrors = mspfe.PushResult.Errors;                                   // record any errors
                }
            }
            catch (Exception exc)
            {
                await App.Current.MainPage.DisplayAlert("Error", exc.Message, "Ok");
            }

            if (syncErrors !=null)
            {
                foreach(var error in syncErrors)
                {
                    // if an error in the copy, revert to the Azure server's copy (check Result not null)
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // if not an  update error (e.g. if local data suddenly missing), discard any local changes
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }
    }
}
