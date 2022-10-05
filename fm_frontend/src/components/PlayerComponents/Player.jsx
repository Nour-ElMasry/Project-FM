import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const Player = () => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();

    useEffect(() => {
        if(user == null){
            navigate("/");
        }
    }, [user, navigate]);

    return <section className='PlayerPage container container--pa'>
        <h1 className="title">Player Page</h1>
    </section>
}

export default Player;