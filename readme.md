
# CODING EXERCISE 

#### Ejecución del proyecto:

`docker-compose up -d `


#### Acceso mediante swagger:

- API DE INFORMACIÓN SOBRE IP: <http://localhost:3001/swagger/index.html>
- API DE ESTADÍSTICAS DE IP:   <http://localhost:9000/swagger/index.html>


#### Ejemplo de llamadas directas al api:
- <http://localhost:3001/IpInfo?ip=1.2.3.4>
- <http://localhost:9000/IpStats>

Notas: 
docker-compose-prod contiene una versión sin swagger para productivo

## Arquitectura
	

### Estructura de proyectos

	**Proyecto base compartido**
	- net-api-core > IpInfoCore > IpInfoCore.csproj

	**Proyecto IpInfo** 
	- net-api > IpInfoService.csproj

	**Test de IpInfo**
	- net-api-test > IpInfoServiceTest.csproj

	**Proyecto IpStats**
	- net-api-stats > IpStatsService > IpStatsService.csproj

	**Test de IpStats**
	- net-api-stats > IpStatsServiceTest > IpStatsServiceTest.csproj

	**Proyecto draft en NodeJS**
	- node-draft


