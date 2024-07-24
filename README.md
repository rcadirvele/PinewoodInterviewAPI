# PinewoodInterviewAPI

## Run Locally

**Step 1 - Database Setup**
  
- *Login using below Credentials and select **DynamoDB** from search,*
  ```bash  
  IAM Login Credentials -
  Account ID (12 digits) or account alias: 464729242637
  Username: PinewoodInterview
  Password: Pinewood@123

  Table Name - customers
  ```

- *Setup **AWS Profile Locally** using below command(Copy + Paste in command prompt),*
    ```bash
    aws configure --profile "PineIntRamAWSProfile"

    AWS Access Key ID: AKIAWYNAFPQGZXIDBPEX
    AWS Secret Access Key: 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
    Default region name: eu-west-2
 
 
**Step 2 - Clone/download below API respos from git Open Pinewood.Customer.API.sln, then Build and Run the API.**
- respos - https://github.com/rcadirvele/PinewoodInterviewAPI.git
- API Swagger URI - https://localhost:20909/swagger/index.html
    
    ***Note:*** I have included above URI in as BaseAddress Blazor Web.


**Step 3 - Clone/download below UI respos from git and Open Pinewood.CustomerInfo.BlazorWasmUI.sln, then Build and Run the Blazor UI.**
- respos - https://github.com/rcadirvele/PinewoodInterviewUI.git
- Blazor URI - https://localhost:7167

    ***Note:*** I have included above URI in API CORS.
