
## Thought Process

The thought process would to be able to test the behaviours of how the diffrent REST API request should behave
when there is 0 or many Pets with a specific ID in the data base.
When there was no pet of a particular id within the database, I decided that it would be best to create,
tests for a post request for invalid cases, and valid cases
for the other requests, GET, PUT and DELETE, when there are zero items with a particular ID, I decides
that there would be a case where the request was invalid such as when the id is not an integer or 
the id was valid but the pet did not exist.

For testing a when there was a pet with a particular I made tests that would confirm whether,