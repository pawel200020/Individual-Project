import {useNavigate, useParams} from "react-router-dom";
import {ReactElement, useEffect, useState} from "react";
import axios, {AxiosResponse} from "axios";
import DisplayErrors from "./DisplayError";
import Loading from "./Loading";

export default function EditEntity<TCreation, TRead>(props: editEntityProps<TCreation, TRead>) {
    const {id}: any = useParams();
    const [entity, setEntity] = useState<TCreation>();
    const [errors, setErrors] = useState<string[]>();
    const navigate = useNavigate();
    useEffect(() => {
        axios.get(`${props.url}/${id}`)
            .then((response: AxiosResponse<TRead>) => {
                setEntity(props.transform(response.data))
            })
    }, [id])

    async function edit(entityToEdit: TCreation) {
        try {
            await axios.put(`${props.url}/${id}`, entityToEdit);
            navigate(props.indexUrl);
        } catch (error: any) {
            if (error && error.response) {
                setErrors(error.response.data);
            }
        }
    }

    return (
        <>
            <h3>Edit {props.entityName}</h3>
            <DisplayErrors errors={errors}/>
            {entity ? props.children(entity, edit): <Loading/>}

        </>
    )
}

interface editEntityProps<TCreation, TRead> {
    url: string

    transform(entity: TRead): TCreation;
    children(entity: TCreation, edit: (entity: TCreation)=> void): ReactElement

    entityName: string

    indexUrl: string
}

EditEntity.defaultProps = {
    transform: (entity: any) => entity
}