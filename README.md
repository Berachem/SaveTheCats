# 🐈 Save The Cats

<aside>
📖 Infographie 3D - IN11 - @ESIEE Paris - E3Fi 2023/2024- Groupe 2I

</aside>

<aside>
👨‍💻 Berachem MARKRIA & Joshua LEMOINE
</aside>

<br/>


🇬🇧 keywords : cats - rescue - cute - fire - relaxation - level


# 🔴 Vidéo de démo

<a href="https://drive.google.com/file/d/1a6l_9oMQ8vYI5YOAFYqly5C747TnJ4-Y/view?usp=sharing"><img  src="https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled 1.png"/></a>

> watch it on [Youtube](https://drive.google.com/file/d/1a6l_9oMQ8vYI5YOAFYqly5C747TnJ4-Y/view?usp=sharing)


## 🐈 Principe du jeu

Vous incarnez un héros dédié au sauvetage de chats égarés dans des villages en proie aux flammes. Votre mission est de localiser ces félins et de les transporter en toute sécurité vers la SafeZone, le tout en visant le temps le plus court possible.

## 💠 Eléments du jeu

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%202.png)

### SafeZone

Il s’agit d’un endroit où ramener les chats. Ces chats seront en sécurité du feu ici ! 

Celle ci utilise des particles.

Cette zone utilise un Box Collider qui lui permet de savoir quand est ce qu’un chat entre ou sors de la Safe Zone.  Ainsi elle peut gérer l’actualisation du nombre de chat a sauver.

### Chat

Il s’agit d’un magnifique animal qui se déplacement aléatoirement, effectuent des miaulement quand il le souhaite. Mais attention… il ne faut pas trop le laisser le rapprocher du feu.

Il possède un script qui lui permet de miauler ou de ronronner aléatoirement. Permettant ainsi au joueur de le retrouver plus facilement

Il est  aussi un Nav-Agent, ce qui lui permet de se déplacer sur le NavMesh de Unity son comportement étant définis par le script WanderingAI

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%203.png)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%204.png)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%205.png)

### Feu

Il y en a un peu partout sur la carte. Attention ça brule !

### Interface Joueur

Gère tout les menus et les affichages du joueur. Ce préfab est très utile car il permet de reproduire rapidement une interface complète dès la création d’un nouveau niveau. 

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%206.png)

### Nav-Mesh

Nous avons utilisé une bibliothèque Unity spécialisé dans le déplacement d’entités : AI Navigation. Il se compose de deux élément principaux : le Nav-Agent dont nous avons rapidement parlé sur le chat et le Nav-Mesh qui représente la zone sur laquelle peut se déplacer nos entité.

Vous avez ici le Nav-Mesh du niveau 1.

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%207.png)

### Le joueur

Ce Préfab est le plus important car il représente le joueur. On voit sa caméra, l’origine des musiques, son “corps” en forme de tic-tac et ces “bras” devant lui (le box collider en forme de rectangle). 

Son script lui permet de se déplacer sur la carte avec aisance, de s’accroupir, de sprinter. Mais aussi de saisir les chat pour pouvoir les déplacer.

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%208.png)

## 🎥 🖼️ Effets de Post-Processing

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%209.png)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2010.png)

### Effets appliqués

**Vignette** 

Intensité : Réglée à 0.549, ce qui détermine la force de l'effet de vignette appliqué aux bords de l'image.
Rondeur : Réglée à 1, indiquant la forme de la vignette. Une valeur de 1 signifie que la vignette est complètement ronde.
Lissage : Réglée à 0.578, indiquant la douceur de la transition entre la vignette et l'image.
**Occlusion ambiante** 

Intensité : Réglée à 1.61, ce qui détermine la force de l'effet d'occlusion ambiante. Plus la valeur est élevée, plus les ombres aux coins et aux creux sont prononcées.

## ⚙️ Optimisations

Nous avons réalisés un effort d’optimisation des maps qui utilisaient un très **grand nombre de Mesh Collider** ce qui pouvait rendre le jeu plus **coûteux en mémoire** qu’il ne devait donc nous sommes ainsi repassé sur les parties de la carte afin de réduire ce nombre et ainsi **gagner en performance !**

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2011.png)

## 📃 Les Scripts

Après avoir fait un tour de tout nos objets les plus important voici l’ensemble des scripts qui les composent :

[SafeZoneController](https://www.notion.so/SafeZoneController-580fecdc7e654d9cbc6c707df0c06185?pvs=21)

[CatController ](https://www.notion.so/CatController-0ec69b9171fb4a349ac7a8aabf2a0a37?pvs=21)

[InterfaceController ](https://www.notion.so/InterfaceController-e1ede140738a47fa9fe755fb735fdf7b?pvs=21)

[ButtonController ](https://www.notion.so/ButtonController-8b074243e6944796a6ac59f0256be36d?pvs=21)

[CustomFirstPersonController ](https://www.notion.so/CustomFirstPersonController-2219372661504ba9b323b5fbe374ffa1?pvs=21)

[WanderingAI](https://www.notion.so/WanderingAI-fb73a9420bd34affa384aef893a9e7cf?pvs=21)

## 🛤️ Cheminement

Lorsque vous démarrez le jeu, vous arrivez sur le menu de démarrage

### 🎹  Menu

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2012.png)

En cliquant sur le bouton “Start”, cela lancera le premier niveau ci-dessous.

### 🌳 Medieval (niveau 1)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2013.png)

 Vous pouvez vous déplacer avec les touches suivantes : 

- Z : avancer
- Q : gauche
- S : reculer
- D : droite
- CTRL : accroupi
- SHIFT : courir
- E : prendre/poser le chat

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2014.png)

Vous devez récupérer tous les chats (2) pour les ramener dans la SafeZone.

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2015.png)

Vous pouvez faire Pause en appuyant sur P : 

- “Resume” reprend le jeu,
- “Main Menu” ramène au menu de départ du jeu
- “Leave” quitte le jeu

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2016.png)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2017.png)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2018.png)

En cliquant sur “Next Level”, vous passerez sur le niveau Pirates

### 🏴‍☠️ Pirates (niveau 2)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2019.png)

Même principe que pour le niveau 1, mais avec cette nouvelle carte et 5 chats et des cachettes un peu plus spéciales 😇.

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2020.png)

![Untitled](https://igadvisory.fr/opendata/SaveTheCats/assets/Untitled%2021.png)

### Et nous voilà arrivé à la fin du jeu ! 🔥😊

Vous trouverez les sources et inspirations utilisés pour la réalisation du projet.

# 🌍 Sources

Il y a eu premièrement le support de cours (Teams) pour se renseigner sur les concepts assez poussés tels que les effets de Post-Processing ou sur l’optimisation d’un jeu mais aussi d’autres éléments où nous en avions besoin !

Et ensuite, les éléments ci-dessous : 

## Assets

### Maps

[RPG Poly Pack - Lite](https://assetstore.unity.com/packages/3d/environments/landscapes/rpg-poly-pack-lite-148410)

[Low Poly Brick Houses](https://assetstore.unity.com/packages/3d/props/exterior/low-poly-brick-houses-131899)

### Props

[Free chibi cat](https://assetstore.unity.com/packages/3d/characters/animals/mammals/free-chibi-cat-165490)

[Particle Light](https://assetstore.unity.com/packages/vfx/shaders/particle-light-10105)

## Fonts

[Pixel Font - Tripfive](https://assetstore.unity.com/packages/2d/fonts/pixel-font-tripfive-64734)

[Arcade Classic Font · 1001 Fonts](https://www.1001fonts.com/arcadeclassic-font.html)

## Sounds

[Free Miaou Sound Effects Download - Pixabay](https://pixabay.com/fr/sound-effects/search/miaou/)

[Free Fire Sound Effects Download - Pixabay](https://pixabay.com/fr/sound-effects/search/fire/)

## Tools

[Unity - Manual:  Unity User Manual 2022.3 (LTS)](https://docs.unity3d.com/Manual/index.html)

[Junior Programmer Pathway - Unity Learn](https://learn.unity.com/learn/pathway/junior-programmer)

[Unity Essentials Pathway - Unity Learn](https://learn.unity.com/pathway/unity-essentials)
