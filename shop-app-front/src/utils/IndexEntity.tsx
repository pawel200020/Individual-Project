import {ReactElement, useEffect, useRef, useState} from "react";
import axios, {AxiosResponse} from "axios";
import {Link} from "react-router-dom";
import RecordsPerPageSelect from "./RecordsPerPageSelect";
import Pagination from "./Pagination";
import GenericList from "./GenericList";
import customConfirm from "./customCofirm";

export default function IndexEntity<T>(props: indexEntityProps<T>) {
    const [entites, setEntities] = useState<T[]>()
    const [totalAmountofPages, setTotalAmountofPages] = useState(0);
    const [recordsPerPage, setRecordsPerPage] = useState(5);
    const [page, setPage] = useState(1);

    const dataFetchedRef = useRef(false);
    useEffect(() => {
        loadData();
    }, [page, recordsPerPage])

    function loadData() {
        if (dataFetchedRef.current) return;
        axios.get(props.url, {params: {page, recordsPerPage}}).then((response: AxiosResponse<T[]>) => {
            const totalAmountofRecords = parseInt(response.headers["totalamountofrecords"], 10);
            setTotalAmountofPages(Math.ceil((totalAmountofRecords / recordsPerPage)));
            setEntities(response.data)
        })
        dataFetchedRef.current = false
    }

    async function deleteEntity(id: number) {
        try {

            await axios.delete(`${props.url}/${id}`).then(() => {
                loadData();
            })
        } catch (error: any) {
            if (error && error.response.data) {
                console.error(error.response.data);
            }
        }
    }

    const buttons = (editUrl: string, id: number) =>
        <>
            <Link className='btn btn-success' to={editUrl}>Edit</Link>
            <button className='btn btn-danger' onClick={() => customConfirm(() => deleteEntity(id))}>Delete</button>
        </>
    return (
        <>
            <h3>{props.title}</h3>
            <Link className='btn btn-primary' to={props.createUrl}>Create {props.entityName}</Link>
            <RecordsPerPageSelect onChange={amountOfRecords => {
                setPage(1);
                setRecordsPerPage((amountOfRecords))
            }
            }/>
            <Pagination currentPage={page} totalAmountOfPages={totalAmountofPages}
                        onChange={newPage => setPage(newPage)}/>
            <GenericList list={entites}>
                <table className='table table-striped'>
                    {props.children(entites!, buttons)}
                </table>
            </GenericList>
        </>
    )
}

interface indexEntityProps<T> {
    url: string
    entityName: string
    createUrl: string
    title: string

    children(entites: T[], buttons: (editUrl: string, id: number) => ReactElement): ReactElement
}