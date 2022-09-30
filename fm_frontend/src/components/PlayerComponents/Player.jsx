import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const Player = () => {
    const user = JSON.parse(localStorage.getItem("User"));
    const navigate = useNavigate();

    useEffect(() => {
        if(user == null){
            navigate("/login");
        }
    }, []);

    return <section className='PlayerPage container container--pa'>
        <h1 className="title">Player Page</h1>
    </section>
}

export default Player;