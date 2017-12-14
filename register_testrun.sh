#!/usr/bin/env bash

if [ -z $1 ]; then
  (>&2 echo "format: $0 <build_id> [url_endpoint]")
  exit 1
fi

BUILD_ID=$1
SCM_REVISION=$2

URL=${3:-"http://testserver-dev.devfactory.com/ts/v2"}

RES=$(curl --fail --silent --show-error -X POST --header 'Content-Type: application/json' --header 'Accept: application/json' \
  -d '{
     "productVersionId": 13874,
     "scmRepoUrl": "https://github.com/pauloadbichara/stateless.git?branch=paulo-mod",
     "scmRepoId": 1420,
     "scmRevision": "'${SCM_REVISION}'",
     "testSuiteId": 1243,
     "buildId": '${BUILD_ID}',
     "executionStartUtc": "2017-12-07T01:00:00.000Z",
     "executionEndUtc": "2017-12-07T01:01:00.000Z",
     "originDetails": "string"
  }' \
  "${URL}/testruns/getorcreate"
)

echo $RES | jq ".testrunId"
