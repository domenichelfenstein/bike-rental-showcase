export const ngPost = async <TOut>(url: string, body: any) : Promise<TOut> => {
    const response = await fetch(
        url,
        {
            method: "POST",
            body: JSON.stringify(body),
            headers: {
                "Content-Type": "application/json"
            }
        });
    const json = await response.json();
    return <TOut>json;
}
