Dieses Repository enthält das Backend für Biletado Reservations, die dazugehörigen YAML-Dateien für die Kubernetes-Integration in die Biletado-Umgebung, sowie die Unit Tests.

Zur installation und Integration des geschriebenen Backends in den Biletado-Cluster muss zuerst das Repository geklont werden.
Anschließend muss im Stammverzeichnis das Kommando ''' kubectl apply -k k8s/api ''' ausgeführt werden. Hierdurch wird automatisch das neueste Image
aus dem Dockerhub verwendet und nahtlos in den Cluster integriert
