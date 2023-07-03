namespace FBDE.Helpers.Enums
{
    public enum Status_DemandeUtilisateur
    {
        Inscription_EnAttenteValidation = 0,
        Desinscription_EnAttenteValidation = 1,
        Actif = 1,
        Inscription_Rejette = 96,
        Desinscription_Rejette = 97,
        Suspendu = 98,
        Supprime = 99,
    }

    public enum Status_DemandeAutorisationModule
    {
        EnAttenteValidation = 0,
        Actif = 1,
        Rejette = 97,
        Suspendu = 98,
        Supprime = 99,
    }

    public enum Status_Notification
    {
        Actif = 1,
        Traite = 2,
        Supprime = 99,
    }
}