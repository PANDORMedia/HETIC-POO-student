# Exercices

Cette page regroupe tous les exercices du bootcamp. Ils sont rangés par jour,
et chaque jour suit la même progression que la classe : on découvre un concept
en leçon, on le teste tout de suite sur un mini exercice, puis on l'applique sur
un exercice plus consistant.

Trois familles d'exercices, à ne pas confondre :

- Les **mini** exercices sont courts et ciblés sur **un seul** concept. On les
  fait juste après la leçon correspondante, souvent en moins de quinze minutes.
  Leur but n'est pas de produire une grosse application, mais de verrouiller une
  intuition (par exemple : "est-ce un héritage ou une composition ?") avant de
  passer à la suite.
- Les **main** exercices (exercices principaux) sont plus longs. Ils combinent
  plusieurs concepts du jour sur un même fil conducteur, le gestionnaire de
  médias, qui grossit de jour en jour. C'est là que tu écris du vrai code.
- Les **corrigés** (fichiers `corrections-formateur`) contiennent une solution
  commentée pour chaque exercice du jour. Ils servent de filet, pas de raccourci.

> Méthode de travail conseillée : ne lis surtout pas le corrigé tout de suite.
> Quand tu bloques, reviens d'abord à la leçon, relis l'énoncé lentement, écris
> le bout de code qui te manque, et fais-le compiler. Tu n'ouvres le corrigé
> qu'après avoir réellement cherché. C'est l'effort de recherche qui ancre le
> concept, pas la lecture de la réponse. Compare ensuite ta solution au corrigé
> pour repérer ce que tu aurais pu faire plus proprement.

---

## Jour 2 : héritage, polymorphisme et interfaces

L'objectif du jour est de cesser de copier-coller du code entre classes
proches. Tu apprends à factoriser ce qui est commun dans une classe parente, à
laisser chaque classe fille se spécialiser, puis à décrire des capacités
transverses avec des interfaces.

Mini exercices (un concept chacun, à faire dans l'ordre) :

- [J2-mini-1 : is-a vs has-a](J2-mini-1-is-a-vs-has-a.md) : calibrer ton intuition pour distinguer une relation "est un" (qui appelle l'héritage) d'une relation "a un" (qui appelle la composition), avant même d'écrire une ligne.
- [J2-mini-2 : new vs override](J2-mini-2-new-vs-override.md) : prédire la sortie d'un programme pour voir, en pratique, pourquoi `new` masque une méthode (et te piège) alors qu'`override` la redéfinit vraiment. Tu y comprends la différence entre le type déclaré et le type réel d'un objet.
- [J2-mini-3 : abstract vs virtual](J2-mini-3-abstract-vs-virtual.md) : choisir le bon modificateur selon qu'une implémentation par défaut a du sens (`virtual`) ou qu'au contraire chaque fille doit obligatoirement fournir la sienne (`abstract`).
- [J2-mini-4 : interface vs classe abstraite](J2-mini-4-interface-vs-abstract.md) : décider entre une interface (une capacité que des classes très différentes peuvent partager) et une classe abstraite (une famille de classes qui partagent du code commun).

Exercices principaux (ils construisent le gestionnaire de médias) :

- [J2-main-1 : refactor en héritage](J2-main-1-heritage-refactor.md) : éliminer la duplication entre `Film`, `Serie` et `Documentaire` en introduisant une classe parente `MediaItem`. C'est ton premier vrai usage de l'héritage sur le fil rouge.
- [J2-main-2 : polymorphisme](J2-main-2-polymorphisme.md) : rendre la méthode `Afficher()` polymorphe sur ta hiérarchie, puis parcourir une `List<MediaItem>` et observer le dispatch dynamique, c'est-à-dire le fait que chaque objet exécute SA version de la méthode.
- [J2-main-3 : MediaItem abstrait](J2-main-3-abstract-mediaitem.md) : transformer `MediaItem` en classe abstraite pour interdire de l'instancier directement et forcer chaque type de média à fournir son propre `Afficher()`.
- [J2-main-4 : IExportable](J2-main-4-iexportable.md) : définir un contrat `IExportable` indépendant de la hiérarchie, l'implémenter différemment sur `Film` et `Serie`, et écrire un service qui ne dépend que de l'abstraction (jamais des classes concrètes).

Corrigé : [J2-corrections-formateur](J2-corrections-formateur.md)

---

## Jour 3 : composition, SOLID et exceptions

Après l'héritage, on apprend à ne PAS en abuser. Le jour 3 montre quand
composer vaut mieux qu'hériter, comment les cinq principes SOLID gardent un code
souple, et comment gérer les erreurs proprement avec des exceptions orientées
objet.

Mini exercices :

- [J3-mini-1 : composition](J3-mini-1-composition.md) : pousser plus loin que le test is-a / has-a du jour 2, sur des cas où le bon choix dépend vraiment du métier et pas seulement de la grammaire.
- [J3-mini-2 : violations SOLID](J3-mini-2-solid-violations.md) : lire des extraits de code et identifier lequel des cinq principes SOLID est violé, en justifiant ton diagnostic en une phrase.
- [J3-mini-3 : flux d'exception](J3-mini-3-flux-exception.md) : maîtriser l'ordre exact d'exécution dans un `try` / `catch` / `finally`, et l'enchaînement entre les `catch` typés et la propagation de l'erreur vers l'appelant.

Exercices principaux :

- [J3-main-1 : refactor en composition](J3-main-1-composition-refactor.md) : sortir une capacité technique d'une classe parente partagée pour l'injecter par composition, et constater le gain de souplesse.
- [J3-main-2 : refactor SOLID](J3-main-2-solid-refactor.md) : décomposer une classe qui fait trop de choses (violation de SRP) et qui dépend de classes concrètes (violation de DIP). Tu extrais les responsabilités et tu inverses les dépendances.
- [J3-main-3 : exception métier](J3-main-3-exception-metier.md) : créer ta propre exception métier qui transporte du contexte utile, pour que le code appelant puisse réagir intelligemment au lieu de planter.

Corrigé : [J3-corrections-formateur](J3-corrections-formateur.md)

---

## Jour 4 : ASP.NET Core MVC

Dernier jour : toute la POO des jours précédents se retrouve dans une vraie
application web. Tu découvres l'architecture MVC (Model, View, Controller) et
l'injection de dépendances, le mécanisme par lequel le framework fournit
lui-même à tes classes les services dont elles ont besoin.

Mini exercices :

- [J4-mini-1 : anatomie d'un projet MVC](J4-mini-1-mvc-anatomy.md) : reconnaître, à la lecture d'un extrait, s'il s'agit d'un Model, d'une View ou d'un Controller, et savoir ce que le runtime ASP.NET Core fait de chacun.
- [J4-mini-2 : lifetimes de l'injection de dépendances](J4-mini-2-di-lifetimes.md) : choisir entre `Singleton`, `Scoped` et `Transient` selon l'état que porte un service et la durée de vie attendue.

Exercice principal :

- [J4-main-1 : ton premier controller](J4-main-1-premier-controller.md) : valider le cycle MVC de bout en bout sur un premier projet, sans injection ni base de données. Juste des Controllers et des Views, pour bien voir le chemin d'une requête.

Corrigé : [J4-corrections-formateur](J4-corrections-formateur.md)

---

## Projets fil rouge

Au-delà des exercices courts, voici trois projets complets, à mener du début à
la fin. Un projet fil rouge n'est pas un exercice de plus : c'est l'occasion
d'assembler tout ce que tu as appris dans une seule application qui tient
debout, avec ses choix d'architecture, ses validations et ses cas d'erreur. Pris
dans l'ordre, ils te font passer de la console au web sur le même domaine
métier.

- [Location de véhicules (console)](location-vehicules.md) : tu construis, en six
  parties qui montent en difficulté, le moteur d'une agence de location. Tu pars
  d'une simple classe `Vehicule` avec ses propriétés validées, tu la rends
  abstraite et tu en dérives `Voiture`, `Moto` et `Camionnette`, tu ajoutes des
  interfaces de capacité (`IAssurable`, `IRevisable`), un menu en ligne de
  commande, une persistance dans un fichier CSV, et enfin, en bonus, un système
  de réservation avec une exception métier et le pattern stratégie pour le calcul
  du tarif. C'est la synthèse console de tout le programme POO.

- [Locatic : agence de location de voitures (mini-projet)](mini-projet.md) : une
  application web **ASP.NET Core MVC** adossée à une base **SQLite** via Entity
  Framework Core. Tu modélises un vrai domaine relationnel (marques, modèles,
  voitures, clients, réservations) avec les liens entre entités, puis tu
  construis les écrans pour tenir le parc à jour. L'accent est mis sur
  l'architecture en couches : domaine, accès aux données, services, controllers
  et vues, sans jamais mélanger la logique métier avec l'affichage.

- [Projet fil rouge : location web (ASP.NET MVC)](J4-projet-fil-rouge-location-web.md) :
  le pont entre les deux mondes. Tu reprends **sans les modifier** les classes
  de domaine écrites dans le projet console, et tu construis autour les couches
  web (Repository, Service, Controllers, Views), phase par phase. Tu vérifies
  ainsi qu'un domaine bien conçu en POO se réutilise tel quel quand on change
  d'interface, ici en passant du terminal au navigateur.
