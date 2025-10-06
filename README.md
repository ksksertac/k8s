
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

1. Push your image to a registry and update `k8s/deployment.yaml` image field.
2. Create the namespace (optional):
```bash
kubectl create ns demo || true
```
3. Apply resources:
```bash
kubectl -n demo apply -f k8s/configmap.yaml
kubectl -n demo apply -f k8s/deployment.yaml
kubectl -n demo apply -f k8s/service.yaml
kubectl -n demo apply -f k8s/hpa.yaml
# Ingress (optional; requires an ingress controller):
kubectl -n demo apply -f k8s/ingress.yaml
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
