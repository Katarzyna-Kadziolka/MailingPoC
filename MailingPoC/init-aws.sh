#!/bin/bash

echo "Verifying SES email addresses..."

awslocal ses verify-email-identity --email noreply@poc.com

awslocal ses list-identities

echo "SES email verification completed!"
