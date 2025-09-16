#!/bin/bash
# Bash criado para verificar se o resource group já foi criado, caso contrario seguir com o fluxo

RESOURCE_GROUP_NAME="rg-burguer404"
LOCATION="East US"
TF_DIR="$(dirname "$0")"  # Diretório atual do script, que já é infra/terraform

if az group exists --name $RESOURCE_GROUP_NAME; then
  echo "Resource group $RESOURCE_GROUP_NAME já existe, importando para o estado do Terraform..."

  # Inicializa o terraform (com backend)
  terraform -chdir="$TF_DIR" init

  # Importa o recurso para o estado
  terraform -chdir="$TF_DIR" import azurerm_resource_group.rg /subscriptions/$(az account show --query id -o tsv)/resourceGroups/$RESOURCE_GROUP_NAME

else
  echo "Resource group $RESOURCE_GROUP_NAME não existe, o Terraform irá criá-lo."
fi
