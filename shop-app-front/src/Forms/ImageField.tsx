import {ChangeEvent, useState} from "react";
import {useFormikContext} from "formik";

export default function ImageField(props: imageFieldProps) {
    const [image, setImage] = useState(``)
    const [imgUrl, setImgUrl] = useState(props.imageUrl)
    const {values} = useFormikContext<any>()
    const divStyle = {marginTop: '20px'}
    const imgStyle = {width: '200px'}
    const handleOnChange = (eventArgs: ChangeEvent<HTMLInputElement>) => {
        if (eventArgs.currentTarget.files) {
            const file = eventArgs.currentTarget.files[0];
            if (file) {
                toBase64(file).then((base64Representation: string) => setImage(base64Representation)).catch(error => console.log(error));
                values[props.filed] = file;
                setImgUrl('');
            } else {
                setImage('');
            }
        }
    }
    const toBase64 = (file: File) => {
        return new Promise<string>((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result as string);
            reader.onerror = (error) => reject(error);

        })
    }
    return (
        <div className='mb-3'>
            <div>
                <input type='File' accept=".jpg,.png,.bmp,.tiff,.jpeg" onChange={handleOnChange}/>
                {image
                    ?
                    <div style={divStyle}>
                        <img style ={imgStyle} src={image} alt="pic"/>
                    </div>
                    :
                    null}
                {imgUrl
                    ?
                    <div style={divStyle}>
                        <img style ={imgStyle} src={imgUrl} alt="pic"/>
                    </div>
                    :
                    null}
            </div>
        </div>
    )
}

interface imageFieldProps {
    displayName: string;
    imageUrl: string;
    filed: string;

}
ImageField.defaultProps ={
    imageUrl: ''
}