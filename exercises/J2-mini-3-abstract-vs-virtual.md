# Mini exo 3 : abstract ou virtual ?

## Objectif

Choisir le bon modificateur selon qu'une implémentation par défaut sensée existe ou pas.

## Énoncé

Pour chacune des 5 méthodes de classe parente ci-dessous, indique si elle devrait être abstract ou virtual, et justifie en une phrase.

1. MediaItem.Afficher() : afficher un média à l'écran.

2. MediaItem.GetUrl() : retourner l'URL "/media/{Id}".

3. Forme.CalculerAire() : calculer l'aire d'une forme géométrique.

4. Notifier.EnvoyerNotification(string msg) : envoyer une notif. Par défaut, juste un log console.

5. Repository<T>.Save(T entity) : sauvegarder une entité (chaque repo a sa techno : SQL, MongoDB, fichier).

## Règle générale

- Si tu peux écrire une implémentation par défaut sensée pour le parent : virtual.
- Sinon : abstract (force l'enfant à fournir son comportement).

## Méthode

Pour chaque méthode, essaie mentalement d'écrire un corps par défaut. Si tu n'en trouves pas qui ait du sens pour TOUS les enfants potentiels, c'est abstract.

## Temps

5 minutes. On corrige collectivement.
