# Exercice principal 3 : Exception métier

## Objectif

Modéliser une exception métier qui transporte du contexte exploitable côté appelant.

## Énoncé

Tu codes un mini système bancaire.

### Étapes

1. Classe `Compte` avec :
   - `Numero` : string
   - `Titulaire` : string
   - `Solde` : decimal, en lecture seule depuis l'extérieur
   - constructeur `Compte(string numero, string titulaire, decimal soldeInitial)`. Refuse un solde initial négatif.

2. Méthode `Crediter(decimal montant)` qui ajoute au solde. Refuse les montants négatifs ou nuls avec une `ArgumentOutOfRangeException`.

3. Crée `SoldeInsuffisantException` qui :
   - hérite de `Exception`
   - porte deux propriétés publiques : `SoldeActuel` (decimal), `MontantDemande` (decimal)
   - a un constructeur `SoldeInsuffisantException(decimal solde, decimal demande)` qui construit un message lisible et stocke les deux valeurs
   - expose une propriété calculée `MontantManquant` qui retourne `MontantDemande - SoldeActuel`

4. Méthode `Retirer(decimal montant)` sur `Compte` qui :
   - refuse les montants négatifs ou nuls (`ArgumentOutOfRangeException`)
   - lance `SoldeInsuffisantException` si le solde ne suffit pas
   - sinon, débite

5. Dans `Program.cs`, crée un compte avec 100 EUR. Crédite 50. Retire 30. Affiche le solde. Tente un retrait de 500 dans un try / catch. Dans le catch, affiche un message qui utilise `MontantManquant`.

## Bonus

Ajoute une méthode `Virer(decimal montant, Compte destinataire)` qui débite ce compte puis crédite l'autre. Si la deuxième opération échoue, le compte source doit revenir à son état initial.

## Temps

15 minutes. Étapes 1 à 5 d'abord, bonus si tu as le temps.
