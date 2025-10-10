
# .NET 8 K8s-Ready Minimal API

A production-ready ASP.NET Core (.NET 8) Minimal API starter packaged for Docker & Kubernetes.
Includes liveness/readiness probes, Swagger, ConfigMap-based configuration, HPA, and optional Ingress.

## Quick Start (Docker)

```bash
docker build -t k8s-ready-api:local -f Dockerfile .
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development k8s-ready-api:local
# Open: http://localhost:8080/swagger
```

## Kubernetes

Apply resources:

```bash

kubectl apply -f k8s/01-namespace.yaml
kubectl apply -f k8s/02-cert-manager.yaml
helm install rancher rancher-latest/rancher --namespace cattle-system --set hostname=k3s.jrkripto.vip --set ingress.tls.source=letsEncrypt --set letsEncrypt.email=abc@abc.com --set letsEncrypt.ingress.class=nginx
kubectl apply -f k8s/04-metrics-server.yaml
kubectl create secret docker-registry ghcr-secret --docker-server=ghcr.io --docker-username=abc --docker-password=ghp_xxxxxxxxxxxxx --docker-email=abc@abc.com
kubectl apply -f k8s/05-testapi-deployment.yaml
kubectl apply -f k8s/06-testapi-service.yaml
kubectl apply -f k8s/07-testapi-hpa.yaml
kubectl apply -f k8s/08-testapi-ingress.yaml

```

### Health endpoints
- Liveness: `/health/live`
- Readiness: `/health/ready`

### Config
`appsettings.json` is embedded. You can override via `k8s/configmap.yaml` which mounts as env vars.

## Helm (optional)
A basic chart is under `helm/k8s-ready-api`. Update values and run:
```bash
helm upgrade --install k8s-ready-api helm/k8s-ready-api -n demo
```
