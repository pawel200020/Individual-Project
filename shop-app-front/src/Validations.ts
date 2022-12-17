import * as Yup from 'yup';
function configValidations(){
    Yup.addMethod(Yup.string,'firstUppercase',function (){
        return this.test('first-letter-upper','First letter must be uppercase',function (value){
            if(value && value.length>0){
                const first = value.substring(0,1);
                return first === first.toUpperCase();
            }
            return true;
        })
    })
}
export default  configValidations;