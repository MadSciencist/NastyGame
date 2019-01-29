export async function apiPost<T>(url: string, data: any): Promise<T> {
  const response = await fetch("http://localhost:5000/api/v1/account/login", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
  });
  if (!response.ok) {
    throw new Error(response.statusText);
  }
  return response.json().then((data) => data as T);
}
