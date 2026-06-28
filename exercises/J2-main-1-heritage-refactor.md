# Exercice principal 1 : Refactor en héritage

## Objectif

Éliminer la duplication entre Film, Série et Documentaire en introduisant une classe parente MediaItem. Premier vrai usage de l'héritage sur le fil rouge du bootcamp.

## Énoncé

Tu pars du code dupliqué de la slide J2.02 : trois classes (Film, Série, Documentaire) qui ont chacune les mêmes champs (Titre, Annee) et la même méthode Afficher().

### Étapes

1. Crée une classe MediaItem avec :
   - propriété publique Titre (string)
   - propriété publique Annee (int)
   - méthode Afficher() qui imprime "Titre (Année)"

2. Donne à MediaItem un constructeur public MediaItem(string titre, int annee).

3. Refais Film pour qu'elle hérite de MediaItem. Ajoute la propriété Realisateur. Son constructeur appelle `: base(titre, annee)`.

4. Idem pour Serie : hérite de MediaItem, ajoute NbSaisons, constructeur avec base(...).

5. Idem pour Documentaire : hérite de MediaItem, ajoute Sujet, constructeur avec base(...).

6. Dans Program.cs, crée un Film, une Série, un Documentaire. Appelle Afficher() sur chacun.

## Bonus

Ajoute la classe Podcast : MediaItem qui ajoute Animateur. Compte combien de lignes tu écris : c'est la mesure exacte du gain de l'héritage.

## Temps

15 minutes. Si tu finis en 10, attaque le bonus. Si tu bloques, commence par construire MediaItem complète AVANT de toucher aux enfants.
