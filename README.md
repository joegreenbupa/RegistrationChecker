# This is an example test excersise based on the following:

The government provide access to vehicle MOT history through an API. You can use it to retrieve vehicle make, model, colour, MOT Test History including reasons for failure and advisory notices. To request the MOT Test History for registration XX10ABC the following request will need to be made: - https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=XX10ABC

This request will require the following request headers:
Value: [{"key":"x-api-key","value":"<insertAPIKeyHere>","description":""}]

## Task requirements

The task is to create an application that accepts a registration number as a parameter. The application should then display the following information about the vehicle: -
-Make
-Model
-Colour
-MOT Expiry Date
-Mileage at last MOT

## User Story

-As a user running the application
-I want to view a vehicle MOT Expiry date if I submit a registration number (e.g. XX10ABC)
-So that I know when to renew the MOT for this vehicle

## Acceptance Criteria
-Given the application prompts the user for a registration number
-When the user submits a valid registration number
-Then the make, model, colour, current MOT expiry date and Mileage at last MOT are displayed

------------------------

##
## NOTES
##


### Decisions:

We were given free choice of platform implementation, i.e console or web app. I did consider writing this
as a React application, but I feel this role is probably more back end at present, and a C# approach is
probably a bit more relevant, so I went with a console app.

### Time taken

Total coding time on this project was approximately 4.5 hours. Also, I spent around 30 mins
preparing before I did anything - sketching out a process flow, thinking about what POCOs I
might need, and also just playing around with the API to see what it returned.

The documentation for the API (This is the government's VES api) is...... about as good as I
expected for the government. Was hoping for a schema of some sort, but instead they settled 
for one example response which doesn't even have half of the fields available. So I had to
repeatedly hit the api with enough valid plates to be fairly confident I had enough examples
that I could use to build an object model for the api response.

I guess 5 hours total time is close enough to be accurate. So this is the approximation of 
just over half days work in actual effort.


### Validation & caveats

I wanted to include a validation section to filter out any requests that we know to be invalid,
so we could discard them before sending to the service. Just because the service can filter out
garbage requests and respond appropriately doesn't mean that it needs to deal with that overhead. 
When scaling up, limiting the number of requests can be a big factor.

In order to validate the given input, I have used a regular expression. This regex is not my own
and was shamelessly copied from the internet. As such, I have not performed any extensive testing
as to it's efficacy. I have a few unit tests which cover some (but certainly not all) possible
formats. For the purposes of this excersise, I am assuming it is reliable.
