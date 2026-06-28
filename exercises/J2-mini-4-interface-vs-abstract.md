# Mini exo 4 : Interface ou classe abstraite ?

## Objectif

Choisir entre interface et abstract class selon que le concept est une capacité transverse ou une famille avec code commun.

## Énoncé

Pour chacun des 6 cas, indique si tu utiliserais une interface ou une classe abstraite, et justifie en une phrase.

1. Tu veux que Film, Date, Score puissent être comparés entre eux (a < b).

2. Tu veux factoriser Titre et Annee pour Film, Série, Documentaire.

3. Tu veux qu'un type puisse être "libéré proprement" (fermer un fichier, une connexion).

4. Tu veux Forme qui partage le code de Couleur et Position, et force chaque sous-classe à fournir CalculerAire().

5. Tu veux qu'un type puisse être sérialisé en JSON par n'importe quel service.

6. Tu veux une famille Animal avec Manger() commun et Crier() que chaque espèce définit.

## Règle de décision

- Code commun à partager : classe abstraite.
- Vraie famille ("est un") : classe abstraite.
- Capacité transverse à la famille : interface.
- Besoin de combiner plusieurs capacités : interface (multi-implémentation).

## Mnémo

Classe abstraite = ADN.
Interface = compétence.

Une seule famille (ADN). Plein de compétences possibles.

## Temps

5 minutes. On corrige collectivement.
