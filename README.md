# BookBinder
This is a bookstore API that allows for the catalog of books, authors and publishers.
It also allows a store manager to keep track of users and the books they have purchased.
The API is hosted [here](https://bookbinder.azurewebsites.net/)
> **Note**
> This API is still a work in progress, the functionalities may change in the near future.

## Endpoints
The endpoints to in use currently are described in this section

## 1. AuthorController 
This controller contains actions performed on the authors

### AddAuthor
You can add an author to the bookstore by inputing the details in the request body
- HTTP Method `POST`
- Route `/api/authors`
- Request Body
  ```
  {
    "firstName": "Freddie",
    "lastName": "Jones",
    "email": "freddie@yahoo.com"
  }
  ```
- Response
  - Returns an `Ok` response with unique id of new author if the request body is correct and author email doesn't exist in the database already.
  -  If request body is incorrect or author exists, it returns a `BadRequest` response with error messages.
  -  If there's a failure commiting to the database, it returns an `InternalServerError` response.
    
### FetchAuthors
This endpoint fetches all authors in the bookstore database
- HTTP Method `GET`
- Route `/api/authors`
- Response
  - Returns an `Ok` response with a list of all authors. Each author is exposed with this `Json` format;
   ```
  {
    "id": "a9852712-124b-4c3b-8ae2-d43b3d126b2b",
    "firstName": "Freddie",
    "lastName": "Jones",
    "email": "freddie@yahoo.com"
  }
  ```
  -  If there's a failure in fetching from the database, it returns an `InternalServerError` response.
    
### UpdateAuthor
You can update the information of an author belonging to the bookstore by inputing the details in the request body
- HTTP Method `PUT`
- Route `/api/authors`
- Request Body
  ```
  {
    "id": "a9852712-124b-4c3b-8ae2-d43b3d126b2b",
    "firstName": "Freddie",
    "lastName": "Jones",
    "email": "freddie@yahoo.com"
  }
  ```
- Response
  - Returns an `Ok` response  if the request body is correct and author exists in the database already.
  -  If request body is incorrect, it returns a `BadRequest` response with an error message.
  -  If author doesn't exist, it returns a `NotFound` response with an error message.
  -  If there's a failure commiting to the database, it returns an `InternalServerError` response.

### RemoveAuthor
This endpoint deletes an existing author from the database.
- HTTP Method `DELETE`
- Route `/api/authors/a9852712-124b-4c3b-8ae2-d43b3d126b2b`
- Response
  - Returns an `Ok` response if the author id is correct and author exists in the database already.
  -  If author id is incorrect, it returns a `BadRequest` response with error message.
  -  If the author doesn't exis it returns a `NotFound` response with error message.
  -  If there's a failure commiting to the database, it returns an `InternalServerError` response.
---------------------------------------------------------------


## Deployment to Azure App Service
This api was deployed to two azure app service plans, windows and linux; this section describes the deployment process for both cases.
### Prerequisites
- Ensure you have an azure account with an active subscription.
- On azure portal create a new resource and select web app.
- Ensure you have basic knowledge of github actions.
### Basics
1. Create a new resource group for your web application i.e. a resource group is used to classify a group of related azure resources that can exchange data between themselves. The resource group for this web app is named bookbinder.
2. Choose the specifics of your application like the runtime stack e.g. `.NET 7`, for the publish options, choose `code`, then choose the region you want your datacenter to be located e.g. `central US`.
3. Pick windows as your operating system if you're deploying to a windows web server or linux for a linux web server.
4. Choose a pricing plan, select the free plan for development; find out more about pricing plans [here](https://azure.microsoft.com/en-us/pricing/details/app-service/windows/).
### Deployment
You can choose to enable github actions from the deployment section or you can configure this after you've created the web app on azure.
If you enable github actions, a workflow automation file will be generated for you by default.
- Select the github account containing the project repository.
- Select an organisation if the project belongs to one.
- Select the branch you want to deploy, in this case the main branch was deployed.
- Preview the workflow file created for you by default;
  - Ensure you have the appropriate trigger(s) for your workflow.
  - Declare any variables you want to use in your workflow file.
  - Change values of some variables to what you would prefer e.g. the output directory for the published application might not be in the directory of your preference.
  - Ensure you're using the correct versions of the actions with their correct inputs.
- You can go ahead and create your app service now.

  > ### Write Workflow Yourself
  > *If you decide to not enable github actions and you want to create the workflow file yourself, do so from github as follows;*
  > - Without enabling github actions, create the appservice.
  > - Navigate to the newly created resource, and click on **download publish profile**.
  > - Copy the downloaded data and enter it as a secret in your github repository.
  > - You can now go to the `Actions` tab of your repo and create a workflow for deployment from scratch or using a template.
  > - In your deployment job section reference your publish profile in the appropriate section like so;
  >   
  > ```
  >   name: Deploy to Azure Web App
  >      uses: azure/webapps-deploy@v2
  >      with:
  >        app-name: 'YOUR_APP'
  >        publish-profile: ${{secrets.YOUR_APP_PUBLISH_PROFILE}}
  >        package: .
  > ```

Trigger the workflow to deploy your application.

> **Note**
> For a linux application you follow basically the same steps as above, but **specify your `OS` as `Linux`both on azure portal and in your workflow file**.
>  - Lastly, in the `General settings` tab of your app configuration on azure portal, set the *start up command* to
>  - ```
>    dotnet your_app.dll
>    ```
>   + This is necessary for bash to known exactly what command to run in order to launch your application.
