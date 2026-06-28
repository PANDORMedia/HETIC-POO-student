# Mini exo 1 : Is-a ou Has-a ?

## Objectif

Calibrer ton intuition pour reconnaître les bonnes relations d'héritage. Avant d'écrire la moindre ligne, on s'assure de distinguer "est un" (héritage) de "a un" (composition).

## Énoncé

Pour chaque paire de concepts ci-dessous, indique s'il s'agit d'une relation is-a (héritage) ou has-a (composition).

1. Voiture / Moteur
2. Voiture / Véhicule
3. Bibliothèque / Livre
4. Chien / Animal
5. Commande / LigneCommande
6. Étudiant / Personne
7. Maison / Pièce
8. Carré / Forme

Pour chaque is-a, écris la déclaration C# correspondante : `class X : Y`.
Pour chaque has-a, indique le champ qui devrait apparaître dans la classe.

## Méthode

Lis à voix haute :

- "Un X est un Y" : si ça sonne juste, c'est is-a (héritage).
- "Un X a un Y" : si ça sonne juste, c'est has-a (composition).

Exemples de calibrage :

- "Une voiture est un moteur" : faux. C'est has-a.
- "Une voiture est un véhicule" : juste. C'est is-a.

## Temps

5 minutes en autonomie, puis débrief collectif.
