# Mini exo 2 : Choisir le bon lifetime

## Objectif

Choisir Singleton, Scoped ou Transient en fonction de l'état et de la portée d'un service.

## Énoncé

Pour chacun des 5 services, indique le lifetime adapté et justifie en une phrase.

1. `IVoitureRepository` qui lit et écrit un fichier JSON unique, contient le catalogue en mémoire.

2. `IEmailService` stateless qui envoie un email via SMTP. Pas d'état conservé entre deux appels.

3. `ICurrentUserService` qui expose l'utilisateur connecté à la requête en cours.

4. `ITarifCalculator` qui calcule un tarif à partir d'un véhicule et d'un nombre de jours. Pas d'état.

5. `IDbContext` (style Entity Framework) qui suit les changements d'entités au sein d'une transaction.

## Règles de décision

- Stateless et léger : Transient.
- Stateless et coûteux à instancier, ou état partagé global : Singleton (avec thread safety si mutable).
- État lié à la requête HTTP en cours : Scoped.

En cas de doute, Scoped reste le défaut le plus sûr.

## Temps

5 minutes. Correction collective.
