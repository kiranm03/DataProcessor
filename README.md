# DataProcessor

# Design and Workflow

1. Web app to upload the files to process. This will upload files to S3.
    url: http://erm-dataupload.s3-website-ap-southeast-2.amazonaws.com/

2. An API is exposed to process files uploaded in step1 and returns transformed result.
    API url: https://73digu5iwa.execute-api.ap-southeast-2.amazonaws.com/dev/api/lp
    API url: https://73digu5iwa.execute-api.ap-southeast-2.amazonaws.com/dev/api/tou

# Prerequisites to run local:
NodeJS, Angular cli, dotnet core 2.1


# Access to solution
