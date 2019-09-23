using Celin.AIS;
using System.Collections.Generic;

namespace blasve.Data.W01012A
{
    public class Form : FormData<Row>
    {
        public FormField<long> z_AN8_21 { get; set; }
        public FormField<string> z_ALPH_28 { get; set; }
        public FormField<string> z_TAX_34 { get; set; }
        public FormField<string> z_AT1_36 { get; set; }
        public FormField<string> z_MLNM_38 { get; set; }
        public FormField<string> z_ADD1_40 { get; set; }
        public FormField<string> z_ADD2_42 { get; set; }
        public FormField<string> z_ADD3_44 { get;set; }
        public FormField<string> z_ADDZ_50 { get; set; }
        public FormField<string> z_CTY1_52 { get; set; }
        public FormField<string> z_ADDS_54 { get; set; }
        public FormField<string> z_CTR_56 { get; set; }
        public FormField<string> z_MCU_62 { get; set; }
    }
    public class Response : Celin.AIS.Response
    {
        public Form<Form> fs_P01012_W01012A { get; set; }
    }
    public class Request : FormRequest
    {
        public void SaveAction(Form form)
        {
            formServiceAction = "U";
            formActions = new List<Action>
            {
                new FormAction { controlID = "28", command = FormAction.SetControlValue, value = form.z_ALPH_28.value },
                new FormAction { controlID = "40", command = FormAction.SetControlValue, value = form.z_ADD1_40.value },
                new FormAction { controlID = "42", command = FormAction.SetControlValue, value = form.z_ADD2_42.value },
                new FormAction { controlID = "11", command = FormAction.DoAction }
            };
        }
        public Request(string an8)
        {
            formName = "P01012_W01012A";
            returnControlIDs = "21|28|34|36|38|40|42|44|50|52|54|56|62";
            formInputs = new List<Input>
            {
                new Input { id = "12", value = an8 }
            };
        }
    }
}
