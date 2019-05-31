# FourplacesApp
_Vincent Renard_  
Application développée via le framework Xamarin.



## Application
### Menu
+ Le menu propose de :
	+ [Se connecter / s'inscrire](#connection)
	+ [Acceder au profil de l'utilisateur (et de le modifier)](#EditProfile)
	+ [Acceder à la liste / carte des lieux répertoriés](#PlacesListView)
	+ [Ajouter des lieux](#NewPlaceView)
	+ Se déconnecter
	 	
+ La connexion déverouille les le menu : 
	+ L'accès au profil
	+ L'ajout de lieux
	+ La déconnexion
	  

 
#### EditProfile  <a name="EditProfile"></a>
Permet de :  

* Consulter ses informations (prénom, nom) et de les modifier  
* Visionner son image de profil et de la modifier  
* Permet de modifier son mot de passe
 
#### PlacesListView  <a name="PlacesListView"></a>
Modélise une carte centrée sur la position de l'utilisateur et localisant les lieux répertoriés.  
Puis liste les lieux avec image.  
Selectionner un lieu accède à sa [description.](#PlaceView)
#### NewPlaceView <a name="NewPlaceView"></a>
[Si l'utilisateur est connecté](#Detection-de-connection) , il est proposé d'ajouter un lieu , la carte de la localisation courante est affichée, l'utilisateur renseigne le titre ,la descrition et une image.

#### PlaceView <a name="PlaceView"></a>
Affiche les information complètes du lieu (titre , descripton ,image) ,la carte 
et la liste des commentaires associés triés par décroissance.
Il est possible d'inserer un commentaire à ce lieu si l'utilisateur est connecté

#### Connection <a name="connection"></a>
Il est possible de se connecter ou bien de s'inscrire le cas échéant.

## Detection de connection <a name="Detection-de-connection"></a>
L'utilisateur est considéré connecté lorsque le login est valide.  
Le module RestService.cs stocke les identifiants courrants. 

 
