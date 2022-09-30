import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const Players = () => {
    const user = JSON.parse(localStorage.getItem("User"));
    const navigate = useNavigate();

    useEffect(() => {
        if(user == null){
            navigate("/login");
        }
    }, []);

    return <section className='PlayersPage container container--pa'>
        <h1 className="title">Players Page</h1>
    </section>
}

export default Players;