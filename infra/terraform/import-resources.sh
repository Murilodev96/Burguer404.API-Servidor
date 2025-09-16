#!/bin/bash
# Bash criado para verificar se o resource group já foi criado, caso contrario seguir com o fluxo

RESOURCE_GROUP_NAME="rg-burguer404"
LOCATION="East US"

# Verifica se o resource group existe
if az group exists --name $RESOURCE_GROUP_NAME; then
  echo "Resource group $RESOURCE_GROUP_NAME já existe, importando para o estado do Terraform..."

  # Importa o resource group para o estado do Terraform
  terraform import azurerm_resource_group.rg /subscriptions/$(az account show --query id -o tsv)/resourceGroups/$RESOURCE_GROUP_NAME

else
  echo "Resource group $RESOURCE_GROUP_NAME não existe, o Terraform irá criá-lo."
fi
