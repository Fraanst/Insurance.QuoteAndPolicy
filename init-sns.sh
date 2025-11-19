#!/bin/bash
# Comando AWS CLI que cria o tópico SNS com o nome 'quote-approved-topic'
awslocal sns create-topic --name quote-approved-topic