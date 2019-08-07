# OpenData Transport
> see https://aka.ms/autorest

This is the AutoRest configuration file for the Transport APIs.

---

#### Basic Information 
These are the global settings for Transport APIs.

``` yaml
# list all the input OpenAPI files (may be YAML, JSON, or Literate- OpenAPI markdown)
#input-file:
#  - http://transport.opendata.ch/swagger.json

# this allows you to programatically tweak the swagger file before it is modeled.
directive:
  from: swagger-document # do it globally 
  where: $.paths.*.* 
  
  # set each operationId to 'Transport_<Tag>'
  transform: $.operationId = `Transport_${$.tags[0]}`
 
```