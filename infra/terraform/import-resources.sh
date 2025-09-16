#!/bin/bash

RESOURCE_GROUP_NAME="rg-burguer404"
CLUSTER_NAME="aks-burguer404"
LOCATION="East US"
TF_DIR="$(dirname "$0")"

# Verifica se o resource group existe
if az group exists --name $RESOURCE_GROUP_NAME; then
  echo "Resource group $RESOURCE_GROUP_NAME já existe, importando para o estado do Terraform..."
  terraform -chdir="$TF_DIR" init
  terraform -chdir="$TF_DIR" import azurerm_resource_group.rg "/subscriptions/$(az account show --query id -o tsv)/resourceGroups/$RESOURCE_GROUP_NAME"
else
  echo "Resource group $RESOURCE_GROUP_NAME não existe, o Terraform irá criá-lo."
fi

# Verifica se o AKS já existe
AKS_EXISTS=$(az aks show --name $CLUSTER_NAME --resource-group $RESOURCE_GROUP_NAME --query "name" -o tsv 2>/dev/null)

if [[ "$AKS_EXISTS" == "$CLUSTER_NAME" ]]; then
  echo "AKS $CLUSTER_NAME já existe, importando para o estado do Terraform..."
  terraform -chdir="$TF_DIR" import azurerm_kubernetes_cluster.aks "/subscriptions/$(az account show --query id -o tsv)/resourceGroups/$RESOURCE_GROUP_NAME/providers/Microsoft.ContainerService/managedClusters/$CLUSTER_NAME"
else
  echo "AKS $CLUSTER_NAME não existe, o Terraform irá criá-lo."
fi
