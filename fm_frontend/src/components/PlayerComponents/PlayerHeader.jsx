const PlayerHeader = (props) => {
    return <div className="personHeaderInfo">
        <img 
            className="personImg" 
            src={props.player?.playerPerson?.image} 
            alt="Player"
        />
        <div className="personName">
            <h2>{props.player?.playerPerson?.name}</h2>
            <p>{props.player?.position}</p>
        </div>
    </div>
}

export default PlayerHeader;

