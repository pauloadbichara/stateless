#!/usr/bin/env bash

if [ -z $1 ]; then
  (>&2 echo "format: $0 <testrun_id> [url_endpoint] [s3|minio_endpoint] [access key] [secret key] [bucket]")
  exit 1
fi

TESTRUN_ID=$1
PROXY_ENDPOINT=${2:-"http://testserver-dev.devfactory.com/ts/v2"}

aws s3 cp coverage.xml s3://testserver-testing/${TESTRUN_ID}/opencover/ --profile test-server
aws s3 cp mapping.json s3://testserver-testing/${TESTRUN_ID}/opencover/ --profile test-server

PREFIX="s3://testserver-testing/${TESTRUN_ID}/opencover"

PAYLOAD='{
  "openCoverXmlUris": [
    "'$PREFIX'/coverage.xml"
  ],
  "sourceFileToDfScmUrlMappingUri": "'$PREFIX'/mapping.json"
}'


RES=$(curl --fail --silent --show-error -X POST --header 'Content-Type: application/json' \
  --header 'Accept: application/json' -d "${PAYLOAD}" \
  "${PROXY_ENDPOINT}/testruns/${TESTRUN_ID}/coverage/opencover")

echo $RES | jq ".artifactSubmissionId"
