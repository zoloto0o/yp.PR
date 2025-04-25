package com.example.ais_abiturient_android;

import android.view.*;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import java.util.List;

public class ApplicantAdapter extends RecyclerView.Adapter<ApplicantAdapter.ViewHolder> {

    private final List<Applicant> applicantList;
    private int selectedPosition = -1;

    public ApplicantAdapter(List<Applicant> applicantList) {
        this.applicantList = applicantList;
    }

    public Applicant getSelectedApplicant() {
        if (selectedPosition != -1) {
            return applicantList.get(selectedPosition);
        }
        return null;
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(android.R.layout.simple_list_item_2, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        Applicant a = applicantList.get(position);
        holder.title.setText(a.fullName);
        holder.subtitle.setText("Документ: " + a.documentType + " №" + a.documentNumber + "\nТел: " + a.phone);

        holder.itemView.setActivated(selectedPosition == position);
        holder.itemView.setOnClickListener(v -> {
            notifyItemChanged(selectedPosition);
            selectedPosition = holder.getAdapterPosition();
            notifyItemChanged(selectedPosition);
        });
    }

    @Override
    public int getItemCount() {
        return applicantList.size();
    }

    static class ViewHolder extends RecyclerView.ViewHolder {
        TextView title, subtitle;
        ViewHolder(View view) {
            super(view);
            title = view.findViewById(android.R.id.text1);
            subtitle = view.findViewById(android.R.id.text2);
        }
    }
}
