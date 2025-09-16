#!/bin/bash
# Bash criado para verificar se o resource group já foi criado, caso contrario seguir com o fluxo

RESOURCE_GROUP_NAME="rg-burguer404"
LOCATION="East US"
TF_DIR="$(dirname "$0")/infra/terraform"  # pega caminho absoluto relativo ao script

if az group exists --name $RESOURCE_GROUP_NAME; then
  echo "Resource group $RESOURCE_GROUP_NAME já existe, importando para o estado do Terraform..."
  
  terraform -chdir="$TF_DIR" import azurerm_resource_group.rg /subscriptions/$(az account show --query id -o tsv)/resourceGroups/$RESOURCE_GROUP_NAME

else
  echo "Resource group $RESOURCE_GROUP_NAME não existe, o Terraform irá criá-lo."
fi
