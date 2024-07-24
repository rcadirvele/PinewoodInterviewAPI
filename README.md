### PinewoodInterview - Basic Cutomer Info. Portal - .Net Core 8.0 API & Mud Blazor UI.

## ----------***Run Locally***------------

**Step 1 - Database Setup**
- AWS Login URI - [Click](https://eu-north-1.signin.aws.amazon.com/oauth?client_id=arn%3Aaws%3Asignin%3A%3A%3Aconsole%2Fcanvas&code_challenge=-m26WQEkVqwsCg763QAYC_Z6rt9OUnppe0_lYUnlgck&code_challenge_method=SHA-256&response_type=code&redirect_uri=https%3A%2F%2Fconsole.aws.amazon.com%2Fconsole%2Fhome%3FhashArgs%3D%2523%26isauthcode%3Dtrue%26nc2%3Dh_ct%26oauthStart%3D1721843669849%26src%3Dheader-signin%26state%3DhashArgsFromTB_eu-north-1_d5317d597a5dba4e)
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

    AWS Access Key ID [None]: AKIAWYNAFPQGZXIDBPEX
    AWS Secret Access Key [None]: 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
    Default region name [None]: eu-west-2
 
 
**Step 2 - Clone/download *[API Respos](https://github.com/rcadirvele/PinewoodInterviewAPI.git)* and Open Pinewood.Customer.API.sln, then Build and Run the API.**

- API Swagger URI - https://localhost:20909/swagger/index.html
    
    ***Note:*** I have included the above URI as a BaseAddress in Blazor Web app.


**Step 3 - Clone/download *[UI Respos](https://github.com/rcadirvele/PinewoodInterviewUI.git)* and Open Pinewood.CustomerInfo.BlazorWasmUI.sln, then Build and Run the Blazor UI.**
- Blazor URI - https://localhost:7167

    ***Note:*** I have included above URI in API CORS.


## -----------***Tech Stack***---------------
### Architecture/Approach -

As software design approrach, followed ***Clean Architecture*** - 

    * Core Layer - Pinewood.Customer.Application.Core.csproj
        * Application
        * Domain
    * Infrastructure Layer - Pinewood.Customer.Infrastructure.csproj
    * Presentation Layer 
        * API - Pinewood.Customer.API.csproj
        * UI  - Pinewood.CustomerInfo.BlazorWasmUI
    
    Unit test: Pinewood.Customer.Test.csproj

#### Technologies used - 

***Client/UI -*** 

- [Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-8.0) Web Assembly with [MudBlzor](https://mudblazor.com/) Components.
    
    **Version** - Asp .Net Core 8.0 .
  
***API technology/packages used -*** 
- .Net Core 8.0 Web API 
- **Validation** - Fluent Validation
- **Logging** - Serilog (File logging & provision for Aws Cloudwatch)
- **Unit Test** - NUnit & Moq as Mock framework.

***Database -*** AWS Cloud DynamoDB *(Provided in setup Instruction).*

I have included all of my approach as i understood.

*****Thanks for the opportunity. Feel free to provide any suggestion. Looking forward to discuss more!*****

 
 
 
 
 ***ToDo*** *- Detailed validation to cover all cases, some UI and include additional features if any.*
