# DataProcessor

# Design and Workflow

1. Web app to upload the files to process. This will upload files to S3.
    url: http://erm-dataupload.s3-website-ap-southeast-2.amazonaws.com/

2. An API is exposed to process files uploaded in step1 and returns transformed result. Returns error message in case of any issues.
    API url: https://73digu5iwa.execute-api.ap-southeast-2.amazonaws.com/dev/api/lp
    API url: https://73digu5iwa.execute-api.ap-southeast-2.amazonaws.com/dev/api/tou

# AWS Services used:
1. S3 static website hosting for Angular 8 web app for upload funtionality.
2. API Gateway and Lambda to expose API to process the data.

# Prerequisites to run local:
NodeJS, Angular cli, dotnet core 2.1, serverless

# Deployment
build file is included in the API project, to deploy serverless app.

# Testing
Use any rest api client tools(eg. Postman) to trigger API. 

# Assumptions
1. Files to be uploaded from trusted source. Hence, data validation of files is not implemented. However, API response will be loaded with error messge to inform the consuming party about the data issues in case of any.

