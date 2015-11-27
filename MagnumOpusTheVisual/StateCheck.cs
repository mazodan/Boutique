using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagnumOpusTheVisual
{
    public static class StateCheck
    {
        /*
         * THE STATECHECK CLASS IS A CLASS THAT CAN STORE THE LOGIC SWITCHES
         * A LOGIC SWITCH IS SOME REFERENCE IF A USER DID SOMETHING
         * 
         * A STATIC CLASS IS UNIQUE, IT CANNOT BE INSTANTIATED
         */

        public static bool logicSwitch { get; set; } //THIS STORES THE BOOLEAN WHETHER THE USER UPDATED OR JUST CLOSED THE FORM
        //DEFAULT VALUES IS FALSE, 

        public static string searchItem { get; set; } 
        //AFTER USER UPDATES/INSERTS GETS THE SEARCH ITEM FROM DBMODIFIER, TO SEARCH IT.

        public static decimal AmtPaid { get; set; }
        //GET AMOUNT PAID

        public static decimal VAT { get; set; }
        //GETS THE VAT

        public static decimal Change { get; set; }
        //GETS THE CHANGE AFTER TRANSACTION IS COMPLETE
    }
}
